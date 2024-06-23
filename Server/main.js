const express = require("express");
require('dotenv').config();
const Test = require('./api/Test/routes');
var cors = require('cors')

const app = express();
app.use(helmet());
const PORT = 3001;


app.use(cors({ credentials: true, origin: 'https://translate-pad.vercel.app/' }));
app.use(helmet.frameguard({ action: 'deny' }));
app.use(helmet.permittedCrossDomainPolicies());
app.use(helmet.noSniff());
app.use(helmet.hidePoweredBy());

app.use(
    csp({
      directives: {
        defaultSrc: ["'self'"],
        scriptSrc: ["'self'", "'unsafe-inline'", "'unsafe-eval'", "https://translate-pad.vercel.app/"],
        styleSrc: ["'self'", "'unsafe-inline'", "https://translate-pad.vercel.app/"],
        imgSrc: ["'self'", "data:", "https://translate-pad.vercel.app/"],
        connectSrc: ["'self'", "https://translate-pad.vercel.app/"],
        fontSrc: ["'self'", "https://translate-pad.vercel.app/"],
        objectSrc: ["'none'"],
        mediaSrc: ["'none'"],
        frameSrc: ["'none'"]
      },
    })
);

app.use((req, res, next) => {
    res.setHeader('Cache-Control', 'no-store, max-age=0');
    next();
});

app.use((req, res, next) => {
    res.removeHeader('Pragma');
    next();
});






app.use(express.json());
app.use("/api/Test", Test);








app.use((req, res, next) => {
  console.log(`Received request for ${req.url}`);
  next();
});


app.listen(PORT, () => {
    console.log(`Server running on port ${PORT}`);
});
