const express = require("express");
const router = express.Router();
const jwt = require('jsonwebtoken');
var cookieParser = require('cookie-parser')
const pg = require("../../cone");
const TokenService =  require("../tokenService");
const bcrypt = require('bcryptjs');

router.use(express.json());
router.use(cookieParser())


router.post('/', async (req, res) => {
    const { email, password } =  await req.body;
    const hashedPassword = await bcrypt.hash(password, 10);
    let client;
    try {
        client = await pg.connect();
        const result = await client.query('SELECT email  FROM trap_users WHERE email = $1;', [email]);
        if (result.rows.length > 0) {
          return res.status(401).json({ message: "Error" });
        }
        else {
            const avatar = "https://54hmmo3zqtgtsusj.public.blob.vercel-storage.com/avatar/Logo-yEeh50niFEmvdLeI2KrIUGzMc6VuWd-a48mfVnSsnjXMEaIOnYOTWIBFOJiB2.jpg"
            const private_status = "false"
            await client.query('INSERT INTO trap_users (email, password, avatar) VALUES ($1, $2, $3, $4);', [email, hashedPassword, avatar]);
            const result = await client.query('SELECT id, email, password, nick FROM trap_users WHERE email = $1;', [email]);
            if(result.rows.length > 0){
              const id_global = result.rows[0].id;
              const values = [email, id_global];
              //const accessToken = await TokenService.generateAccessToken(values);
              const refreshToken = await TokenService.generateRefreshToken(values);
              await client.query('INSERT INTO trap_users (email, token_refresh) VALUES ($1);', [email, refreshToken]);
              return res.status(200).json({refreshToken, userPreferences: { theme: 'dark', language: 'ua' }});
            }
            //localStorage.setItem('accessToken', accessToken);
      
            return res.status(404).json({ message: "Error" });
          }
    } catch (error) {
        return res.status(500).json({ error: 'Internal Server Error ' + error.message });
    } finally {
        await client.end();
    }
});

module.exports = router;