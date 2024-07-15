const express = require("express");

const router = express.Router();
router.use(express.json());


router.get('/', async (req, res) => {
    try {

        return res.status(200).json({ message: "ok" });
    } catch (error) {
        console.error('Internal Server Error:', error);
        return res.status(500).json({ error: 'Internal Server Error' });
    }
});

module.exports = router;