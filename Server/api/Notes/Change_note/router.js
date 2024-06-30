const express = require("express");
const router = express.Router();
const jwt = require('jsonwebtoken');
const pg = require("../../cone");

router.use(express.json());


router.post('/', async (req, res) => {
    const { id, title, content } = req.body;
    let client;
    try {
        const result = await client.query('UPDATE public.trap_notes SET title, content VALUES ($1, $2) WHERE id = $3;', [title, content, id]);
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