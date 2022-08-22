import { Sajam } from "./Sajam.js";
import { Kompanija } from "./Kompanija.js";
import { Sala } from "./Sala.js";


export class Sajmovi{
    constructor(listaSajmova){
        this.listaSajmova = listaSajmova;
        this.kontejner = null;
    }

    crtajSajmove(host){

        this.kontejner=document.createElement("div");
        this.kontejner.className="PocetniKontejner";
        host.appendChild(this.kontejner);

        let kontForma = document.createElement("div");
        kontForma.className="KontForma";
        this.kontejner.appendChild(kontForma);

        this.crtajformu(kontForma);

        let velikaForma = document.createElement("div");
        velikaForma.className="velikaForma";
        this.kontejner.appendChild(velikaForma);

    }
    

    crtajformu(host)
    {
        let labela = document.createElement("h4");
        labela.innerHTML="Izaberite sajam:";
        host.appendChild(labela);

        var se = document.createElement("select");
        se.className="selectSajam";
        host.appendChild(se);

        let op;
        this.listaSajmova.forEach(p => {
            op=document.createElement("option");
            op.innerHTML=p.naziv;
            op.value=p.id;
            se.appendChild(op);
            
        });

        let red = document.createElement("div");
        
        host.appendChild(red);

        let dugme = document.createElement("button");
        dugme.className="dugmeOsnovno";
        dugme.innerHTML="OK";
        dugme.onclick=(ev)=>this.ucitajSajam();
        red.appendChild(dugme);

       
    }

    

    ucitajSajam() //crta veliku formu
    {
        let optionEl = this.kontejner.querySelector("select");
        var sajamID = optionEl.options[optionEl.selectedIndex].value;
        var velikaForma= this.ObrisiPrethodniSajam();
        var sajam = new Sajam(sajamID, " ");
        sajam.crtajSajam(velikaForma);
    }

    UveziKompanije2(sajamID){
       
            let listaKompanija2=[];
       
            fetch("https://localhost:5001/Sajam/PreuzetiKompanije/"+sajamID,
            {
                method:"GET"
            }).then(s=>{
                if(s.ok){
                
                    s.json().then(data=>{
                        data.forEach(s=>{
                            let pr = new Kompanija(s.id,s.naziv,s.hr,s.oblast,s.godinaOsnivanja);  
                            listaKompanija2.push(pr);
                        })
                       
                    })
                }
              
                
            })
      
        
    }

    UveziSale2(sajamID){

        var listaSala=[];
        
        fetch("https://localhost:5001/Sajam/PreuzetiSale/"+sajamID,
        {
            method:"GET"
        }).then(s=>{
            if(s.ok){
               
                s.json().then(data=>{
                    data.forEach(s=>{
                        let pr = new Sala(s.id,s.naziv);                         
                        listaSala.push(pr);
                    })
                })
            }
        })

        return listaSala;

    }

    ObrisiPrethodniSajam()
    {
        let velikaForma = this.kontejner.querySelector(".velikaForma");
        let roditelj = velikaForma.parentNode;
        roditelj.removeChild(velikaForma);

        velikaForma = document.createElement("div");
        velikaForma.className="velikaForma";
        roditelj.appendChild(velikaForma);
        return velikaForma;

    }
}


