const express = require("express");
const router = express.Router();
const jwt = require('jsonwebtoken');
const pg = require("../../cone");
router.use(express.json());

router.post('/', async (req, res) => {
    const { token } = req.body;
    let client;
    try {
        client = await pg.connect();
        if (token) {
            // Додано: перевірка на наявність секретного ключа
            if (!process.env.JWT_SECRET) {
                throw new Error("JWT_SECRET is not defined in environment variables");
            }
            const jwtres = jwt.verify(token, process.env.JWT_SECRET);
            const id = jwtres.data[1];
            if (typeof jwtres === 'object' && jwtres !== null) {
                const result = await client.query('SELECT * FROM public.trap_notes WHERE user_id = $1 ORDER BY updated_at DESC;', [id]);
                const rows = result.rows;
                return res.status(200).json(rows);
            }
        }
        return res.status(400).json({ error: 'None Token' });
    } 
    catch (error) {
        return res.status(500).json({ error: 'Internal Server Error: ' + error.message });
    }   
    finally {
        if (client) {
            client.release(); 
        }
    }
});

module.exports = router;
