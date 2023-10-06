import { Film } from "./Film.js";
import { Kategorija } from "./Kategorija.js";

export class Produkcija {
    constructor(id, nazivPro) {
        this.id = id;
        this.nazivPro = nazivPro;
        this.listaKat = [];
        this.container = null;
    }

    dodajKategoriju(kat) {
        this.listaKat.push(kat);
    }

    crtajProdukciju(host) {
        this.container = document.createElement("div");
        this.container.className = "glavniDiv";
        host.appendChild(this.container);

        var gornji = document.createElement("h1");
        gornji.innerHTML = this.nazivPro;
        gornji.className = "gornji";
        this.container.appendChild(gornji);

        var srednji = document.createElement("div");
        srednji.className = "srednji";
        this.container.appendChild(srednji);

        var srednjiLevi = document.createElement("div");
        srednjiLevi.className = "srednjiLevi";
        srednji.appendChild(srednjiLevi);
        //=====================================================Labela

        var labelaKat = document.createElement("label");
        labelaKat.innerHTML = "Kategorija: ";
        labelaKat.className = "labelaKat";
        srednjiLevi.appendChild(labelaKat);

        var labelaFilm = document.createElement("label");
        labelaFilm.innerHTML = "Film: ";
        labelaFilm.className = "labelaFilm";
        srednjiLevi.appendChild(labelaFilm);

        var labelaOcena = document.createElement("label");
        labelaOcena.innerHTML = "Ocena: ";
        labelaOcena.className = "labelaOcena";
        srednjiLevi.appendChild(labelaOcena);
        //=====================================================Labela

        var srednjiDesni = document.createElement("div");
        srednjiDesni.className = "srednjiDesni";
        srednji.appendChild(srednjiDesni);

        var inputKat = document.createElement("select");
        inputKat.id = this.id;
        inputKat.className = "inputKat";
        srednjiDesni.appendChild(inputKat);
        this.listaKat.forEach((o, index) => {
            const opcija = document.createElement("option");
            opcija.innerHTML = o.nazivKat;
            opcija.value = index;
            if (index != null) {
                opcija.selected = true;
            }
            inputKat.appendChild(opcija);
        });
        inputKat.onclick = (ev) => this.prikaziFilmove();

        let filmInput = document.createElement("select");
        filmInput.className = "filmInput";
        srednjiDesni.appendChild(filmInput);

        let ocenaInput = document.createElement("input");
        ocenaInput.type = "number";
        ocenaInput.min = 1;
        ocenaInput.max = 10;
        ocenaInput.value = 1;
        ocenaInput.className = "ocenaInput";
        srednjiDesni.appendChild(ocenaInput);

        let dugme = document.createElement("button");
        dugme.innerHTML = "Snimi ocenu";
        dugme.className = "dugme";
        srednjiDesni.appendChild(dugme);

        //=====================================================Input-i

        var donji = document.createElement("div");
        donji.className = "donji";
        this.container.appendChild(donji);

        //this.prikaziGraf(donji);

        

        //=====================================================Donji
    }

    prikaziFilmove() {
        let idKat = parseInt(this.container.querySelector(".inputKat").value);
        console.log(idKat);
        this.preuzmiFilmove(idKat);
    }

    // Pretpostavka: this.listaKat je niz objekata tipa Kategorija
    // Gde svaki objekat Kategorija ima svojstvo filmovi koje je niz filmova

    async preuzmiFilmove(idKat) {
        if (this.listaKat[idKat]) {
            const id = this.listaKat[idKat].id;
            console.log("asd", id);

            var response = await fetch(`http://localhost:5078/Kategorija/VratiSveFilmoveOveKategorije/${id}`);
            var data = await response.json();
            console.log("Data:", data);

            if (data && data.filmovi) {
                const filmovi = data.filmovi;
                // Provera i inicijalizacija niza filmovi ako je potrebno
                //if (!this.listaKat[idKat].filmovi) {
                this.listaKat[idKat].filmovi = [];
                //}
                filmovi.forEach(async p => {
                    console.log(p);
                    let film = new Film(p.id, p.nazivFilma, p.ocena);
                    this.listaKat[idKat].filmovi.push(film);
                });
            } else {
                console.log("Nema podataka o filmovima.");
            }

            let filmSelector = this.container.querySelector(".filmInput");
            filmSelector.innerHTML = "";
            this.popuniFilmove(filmSelector, this.listaKat[idKat].filmovi);
            // azurira grafikon
            this.prikaziGrafikon(this.listaKat[idKat].id);
        } else {
            console.log(`Nema kategorije sa indeksom ${idKat}.`);
        }
    }



    popuniFilmove(filmSel, filmovi) {
        console.log(filmovi);
        filmovi.forEach((film, index) => {
            const opcija = document.createElement("option");
            opcija.innerHTML = film.nazivFilma;
            opcija.value = index;
            filmSel.appendChild(opcija);
        })

    }

    async prikaziGrafikon(id){
        var donji = this.container.querySelector(".donji");
        donji.innerHTML = "";
        //let id = parseInt(this.container.querySelector(".inputKat").value);
        console.log(id);
        var response = await fetch(`http://localhost:5078/Kategorija/VratiBrojFilmova/${id}`);
        var data = await response.json();
        console.log("3333333333", data);
        
        if (data && Array.isArray(data)) { // Provera da li je data validan niz
            data.forEach(p => {
                let film = new Film(p.id, p.nazivFilma, p.ocena);
                film.crtajFilm(donji);
            });
        } else {
            console.log("Nema podataka o filmovima.");
        }
        
        
        // filmovi.forEach(film => {
        //     film.crtajFilm(donji);
        // })
    }

}