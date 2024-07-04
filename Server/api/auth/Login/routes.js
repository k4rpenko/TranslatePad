const express = require("express");
const router = express.Router();
const pg = require("../../cone");
const bcrypt = require('bcryptjs');
var cookieParser = require('cookie-parser')
const jwt = require('jsonwebtoken');


router.use(express.json());
router.use(cookieParser());

router.get('/', async (req, res) => {
    let client;
    try {
        client = await pg.connect();
        const refreshToken = req.cookies['auth_token'];
        if (refreshToken) {
            const jwtres = jwt.verify(refreshToken, process.env.JWT_SECRET);
            const id = jwtres.data[1];
            if (typeof jwtres === 'object' && jwtres !== null) {
                const result = await client.query('SELECT * FROM public.trap_users WHERE id = $1;', [id]);
                const resultT = await client.query('SELECT * FROM public.trap_translations WHERE user_id = $1;', [id]);
                const resultN = await client.query('SELECT * FROM public.trap_notes WHERE user_id = $1;', [id]);
                const rowCount = result.rowCount;
                const rowCountT = resultT.rowCount;
                const rowCountN = resultN.rowCount;
                const Avatar = result.rows[0].avatar;
                const Email = result.rows[0].email;
                return res.status(200).json({ Avatar, Email, rowCount, rowCountT, rowCountN });
            }
        } else {
            return res.status(400).json({ error: 'No cookie found' });
        }
    } catch (error) {
        return res.status(500).json({ error: 'Internal Server Error ' + error.message });
    } finally {
        if (client) {
            client.release(); 
        }
    }
});


router.post('/', async (req, res) => {
    const { email, password } = req.body;
    let client;
    try {
        client = await pg.connect();

        const result = await client.query('SELECT * FROM public.trap_users WHERE email = $1;', [email]);

        if (result.rows.length > 0) {
            const dbPassword = result.rows[0].password;
            const passwordMatch = await bcrypt.compare(password, dbPassword);
            if (!passwordMatch) {
              return res.status(401).json({ message: "Error" });
            }
            const refreshToken = result.rows[0].token_refresh;    
            return res.status(200).json({ refreshToken });
        } 
        else {
            return res.status(404).json({ message: "User not found" });
        }
    } catch (error) {
        return res.status(500).json({ error: 'Internal Server Error ' + error.message });
    } finally {
        if (client) {
            client.release(); 
        }
    }
});

module.exports = router;