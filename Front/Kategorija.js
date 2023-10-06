export class Kategorija{
    constructor(id, nazivKat){
        this.id = id;
        this.nazivKat = nazivKat;
        this.listaFilmova = [];
        this.container = null;
    }

    dodajFilm(film){
        this.listaFilmova.push(film);
    }


}