import { Sajmovi } from "./Sajmovi.js";
import { Sajam } from "./Sajam.js";

var listaSajmova = [];

fetch("https://localhost:5001/Sajam/PrikaziSajmove")
.then(
    p=>{
        p.json().then(sajmovi => {
            sajmovi.forEach(sajam => {
                var p = new Sajam(sajam.id, sajam.naziv);
                
                listaSajmova.push(p);
            });
            
            var sajmovi = new Sajmovi(listaSajmova);
            sajmovi.crtajSajmove(document.body);
            
        })
    }
)


