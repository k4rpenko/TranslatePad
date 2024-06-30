const express = require("express");
const router = express.Router();
const jwt = require('jsonwebtoken');
const pg = require("../../cone");

router.use(express.json());


router.post('/', async (req, res) => {
    const { token, title, content } = req.body;
    let client;
    try {
        console.log(req.body);
        client = await pg.connect();
        if (token) {
            const jwtres = jwt.verify(token, process.env.JWT_SECRET);
            const id = jwtres.data[1];
            if (typeof jwtres === 'object' && jwtres !== null) {
                console.log(id);
                const result = await client.query('INSERT INTO public.trap_notes (user_id, title, content) VALUES ($1, $2, $3);', [id, title, content]);
                return res.status(200).json({ message: 'Creat' });
            }
        }
        return res.status(400).json({ error: 'None Token' });
    } 
    catch (error) {return res.status(500).json({ error: 'Internal Server Error ' + error.message });}   
    finally {
        if (client) {
            client.release(); 
        }
    }
});

module.exports = router;