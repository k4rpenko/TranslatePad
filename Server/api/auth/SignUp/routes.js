const express = require("express");
const router = express.Router();
const jwt = require('jsonwebtoken');
var cookieParser = require('cookie-parser')
const pg = require("../../cone");
const TokenService =  require("../tokenService");
const bcrypt = require('bcryptjs');

router.use(express.json());
router.use(cookieParser())
router.get('/', async (req, res) => {
    let client;
    try {
        client = await pg.connect();
        const resultP = await client.query('SELECT * FROM public.trap_users WHERE id > 0;');
        const resultT = await client.query('SELECT * FROM public.trap_translations WHERE id > 0;');
        const resultN = await client.query('SELECT * FROM public.trap_notes WHERE id > 0;');
        const rowCount = resultP.rowCount;
        const rowCountT = resultT.rowCount;
        const rowCountN = resultN.rowCount;
        return res.status(200).json({rowCount, rowCountT, rowCountN});
    } catch (error) {
        return res.status(500).json({ error: 'Internal Server Error ' + error.message });
    } finally {
        if (client) {
            client.release(); 
        }
    }
});

router.post('/', async (req, res) => {
    const { email, password } =  await req.body;
    const hashedPassword = await bcrypt.hash(password, 10);
    console.log("Received email:", email); 
    console.log("Received password:", password);
    console.log("bcrypt password:", hashedPassword);
    let client;
    try {
        client = await pg.connect();
        const result = await client.query('SELECT email FROM trap_users WHERE email = $1;', [email]);
        if (result.rows.length > 0) {
          return res.status(401).json({ message: "Error" });
        }
        else {
            const avatar = "https://54hmmo3zqtgtsusj.public.blob.vercel-storage.com/avatar/Logo-yEeh50niFEmvdLeI2KrIUGzMc6VuWd-a48mfVnSsnjXMEaIOnYOTWIBFOJiB2.jpg"
            await client.query('INSERT INTO trap_users (email, password, avatar) VALUES ($1, $2, $3);', [email, hashedPassword, avatar]);
            const result = await client.query('SELECT * FROM trap_users WHERE email = $1;', [email]);
            const id = result.rows[0].id;
            const values = [email, id];
            //const accessToken = await TokenService.generateAccessToken(values);
            const refreshToken = await TokenService.generateRefreshToken(values);
            await client.query('UPDATE trap_users SET token_refresh = $1 WHERE id = $2;', [refreshToken, id]);
            return res.status(200).json({refreshToken});
            //localStorage.setItem('accessToken', accessToken);
          }
    } catch (error) {
        return res.status(500).json({ error: 'Internal Server Error ' + error.message });
    } finally {
        await client.end();
    }
});

module.exports = router;