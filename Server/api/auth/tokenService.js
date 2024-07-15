const jwt = require("jsonwebtoken");

class TokenService {
    static generateAccessToken(v) {
        return jwt.sign({ data: v }, process.env.JWT_SECRET, { expiresIn: '30m' }); // Expires in 30 minutes
    };

    static generateRefreshToken(v) {
        return jwt.sign({ data: v }, process.env.JWT_SECRET, { expiresIn: '30d' }); // Expires in 30 days
    };
}

module.exports = TokenService;
