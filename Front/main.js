import { Produkcija } from "./Produkcija.js";
import {Kategorija} from "./Kategorija.js"

var response = await fetch("http://localhost:5078/Produkcija/VratiSveProdukcije");
var data = await response.json();
data.forEach(async p => {
    var produkcija = new Produkcija(p["id"], p["nazivPro"]);
    var kategorijee = p["kategorije"];
    kategorijee.forEach(k => {
        var kategorija = new Kategorija(k["id"], k["nazivKat"]);
        produkcija.dodajKategoriju(kategorija);
    })
    console.log(produkcija);
    produkcija.crtajProdukciju(document.body);
});
console.log(response);


// var response = await fetch("http://localhost:5240/VideoKlub/VratiSveVideoKlubove");
// var data = await response.json();

// data.forEach(async obj => {