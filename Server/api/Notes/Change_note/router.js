const express = require("express");
const router = express.Router();
const jwt = require('jsonwebtoken');
const pg = require("../../cone");

router.use(express.json());


router.post('/', async (req, res) => {
    const { id, title, content, updated_at } = req.body;
    let client;
    try {
        client = await pg.connect();
        const result = await client.query('UPDATE public.trap_notes SET title = $1, content = $2, updated_at = $3 WHERE id = $4;', [title, content, updated_at, id]);
        return res.status(200).json({ message: 'Change' });
    } 
    catch (error) {return res.status(500).json({ error: 'Internal Server Error ' + error.message });}   
    finally {
        if (client) {
            client.release(); 
        }
    }
});

module.exports = router;