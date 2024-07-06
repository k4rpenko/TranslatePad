const express = require("express");
const router = express.Router();
const jwt = require('jsonwebtoken');
const pg = require("../../cone");

router.use(express.json());

router.post('/', async (req, res) => {
    const { id } = req.body;
    let client;
    try {
        client = await pg.connect();
        const result = await client.query('SELECT * FROM public.trap_notes WHERE id = $1 ORDER BY updated_at DESC;', [id]);
        const rows = result.rows;
        return res.status(200).json(rows);
    } 
    catch (error) {return res.status(500).json({ error: 'Internal Server Error ' + error.message });}   
    finally {
        if (client) {
            client.release(); 
        }
    }
});

module.exports = router;