const express = require("express");
const router = express.Router();
const pg = require("../../cone");
const bcrypt = require('bcryptjs');

router.use(express.json());

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