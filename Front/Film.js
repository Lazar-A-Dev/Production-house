export class Film{
    constructor(id, nazivFilma, ocena){
        this.id = id;
        this.nazivFilma = nazivFilma;
        this.ocena = ocena;
        this.container = null;
    }

    crtajFilm(host){
        var prikaz = document.createElement("div");
        prikaz.className = "prikaz";
        host.appendChild(prikaz);

        var labNazF = document.createElement("label");
        labNazF.className = "labNazF";
        labNazF.innerHTML = this.nazivFilma;
        prikaz.appendChild(labNazF);

        var vizualniDeo = document.createElement("div");
        vizualniDeo.className = "vizualniDeo";
        prikaz.appendChild(vizualniDeo);

        var popuna = document.createElement("div");
        popuna.style.flexGrow = this.ocena/10;
        popuna.className = "popuna";
        vizualniDeo.appendChild(popuna);

        var labOcena = document.createElement("div");
        labOcena.innerHTML = this.ocena;
        labOcena.className = "labOcena";
        prikaz.appendChild(labOcena);
    }
}