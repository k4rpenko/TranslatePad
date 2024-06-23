const express = require("express");
const pg = require("../cone");

const router = express.Router();
router.use(express.json());
router.use(cookieParser())

router.get('/', async (req, res) => {
    try {
        return res.status(200).json({ message: "ok" });
    } catch (error) {
        return res.status(500).json({ error: 'Internal Server Error: ' + error.message });
    } finally {
        if (client) {
            client.release(); 
        }
    }
});