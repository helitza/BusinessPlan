import { Kompanija } from "./Kompanija.js";
import { Prezentovanje } from "./Prezentovanje.js";
import { Sala } from "./Sala.js";
export class Sajam{
    constructor(id, naziv)
    {
        this.id=id;
        this.naziv=naziv;
        this.listaSala=[];
        this.listaKompanija=[];
        this.listaPrezentovanja = [];
        this.kontejner=null;
        
       
        
    }

    crtajSajam(host)
    {
        
        // this.kontejner=document.createElement("div");
        // this.kontejner.className="PocetniKontejnerSajam";
        // host.appendChild(this.kontejner);

        this.kontejner=host;

        let kompanijeForma = document.createElement("div");
        kompanijeForma.className="KompanijeForma";
        this.kontejner.appendChild(kompanijeForma);

        let pomocnaForma = document.createElement("div");
        pomocnaForma.className="PomocnaForma";
        this.kontejner.appendChild(pomocnaForma);

        let prezentovanjeForma = document.createElement("div");
        prezentovanjeForma.className="PrezentovanjeForma";
        pomocnaForma.appendChild(prezentovanjeForma);

        let rasporedForma = document.createElement("div");
        rasporedForma.className="RasporedForma";
        pomocnaForma.appendChild(rasporedForma);

        this.crtajKompanije(kompanijeForma, prezentovanjeForma);
        this.crtajRaspored(rasporedForma);
       


    }

    crtajKompanije(host,host2){

        
        let labela = document.createElement("h3");
        labela.className="Podnaslov";
        labela.innerHTML="ŠTANDOVI KOMPANIJA";
        host.appendChild(labela);

        let kontKompanije = document.createElement("div");
        kontKompanije.className="Kompanije";        
        host.appendChild(kontKompanije);
        
        this.ucitajKompanije(kontKompanije, host2);

    }

    ucitajKompanije(host, host2){

        
        fetch("https://localhost:5001/Sajam/PreuzetiKompanije/"+this.id, {
            method:"GET"
        }).then(s => {
            if(s.ok) {
                s.json().then(data => {
                    data.forEach(s => {
                        let pr = new Kompanija(s.id,s.naziv,s.hr,s.oblast,s.godinaOsnivanja);  
                        this.listaKompanija.push(pr);         
                        pr.crtajKompaniju(host);
                    });
                   this.crtajFormuPrezentovanje(host2);
                    
                    
                });
            }
            
           
        })

    }

    crtajRaspored(host)
    {
        let dugme = document.createElement("button");
        dugme.innerHTML="RASPORED PREZENTACIJA";
        dugme.className="DugmeRaspored";
        host.appendChild(dugme);
        let kontRaspored = document.createElement("div");
        kontRaspored.className="Kompanije";        
        host.appendChild(kontRaspored);
        this.crtajTabeluRaspored(kontRaspored);
        dugme.onclick=(ev)=>this.ucitajRaspored();
       
        
    }

    ucitajRaspored(){
        
        fetch("https://localhost:5001/Prezentovanje/PreuzetiRasporedSajma/"+this.id,
        {
            method:"GET"
        }).then(s=>{
            if(s.ok){

                var rasporedTeloTabele=this.ObrisiPrethodniRaspored();
                s.json().then(data=>{
                    data.forEach(s=>{
                        let pr = new Prezentovanje(s.id,s.datum,s.vreme,s.sala,s.kompanija);                         
                        pr.crtajPrezentovanjeRed(rasporedTeloTabele);
                    })
                })
            }
        })
    }
    

    ObrisiPrethodniRaspored()
    {
        let rasporedTeloTabele = document.querySelector(".RasporedTeloTabele");
        let roditelj = rasporedTeloTabele.parentNode;
        roditelj.removeChild(rasporedTeloTabele);

        rasporedTeloTabele = document.createElement("tbody");
       rasporedTeloTabele.className="RasporedTeloTabele";
        roditelj.appendChild(rasporedTeloTabele);
        return rasporedTeloTabele;
    }


    crtajTabeluRaspored(host){

        var tabela = document.createElement("table");
        host.appendChild(tabela);

        var tabelahead= document.createElement("thead");
        tabela.appendChild(tabelahead);

        var tr = document.createElement("tr");
        tabelahead.appendChild(tr);

        var tabelaBody = document.createElement("tbody");
        tabelaBody.className="RasporedTeloTabele";
        tabela.appendChild(tabelaBody);

        let th;
        var zag=["Datum", "Vreme", "Sala", "Kompanija", "Uredi"];
        zag.forEach(el=>{
            th = document.createElement("th");
            th.innerHTML=el;
            tr.appendChild(th);
        })
    }

    crtajFormuPrezentovanje(host)
    {
        let labela = document.createElement("h3");
        labela.innerHTML="PREZENTOVANJE";
        host.appendChild(labela);

        let minikont1 = document.createElement("div");
        minikont1.className="minikont";
        host.appendChild(minikont1);

        labela = document.createElement("label");
        labela.innerHTML="Kompanija:";
        minikont1.appendChild(labela);

        
        var se = document.createElement("select");
        se.className="kompanijaSelect"
        minikont1.appendChild(se);

        let op;
        this.listaKompanija.forEach(p => {
            
            op=document.createElement("option");
            op.innerHTML=p.naziv;
            op.value=p.id;
            se.appendChild(op);
            
        });

        let minikont2 = document.createElement("div");
        minikont2.className="minikont";
        host.appendChild(minikont2);
        

        let minikont3 = document.createElement("div");
        minikont3.className="minikont";
        host.appendChild(minikont3);
        
        let minikont4 = document.createElement("div");
        minikont4.className="minikont";
        host.appendChild(minikont4);

        let labela2b = document.createElement("label");
        labela2b.innerHTML="Datum:";
        minikont2.appendChild(labela2b);
        let datumPoljeB = document.createElement("input");
        datumPoljeB.className = "datumPolje2";
        datumPoljeB.type = "date";
        minikont2.appendChild(datumPoljeB);

        labela2b = document.createElement("label");
        labela2b.innerHTML="Vreme:";
        minikont2.appendChild(labela2b);
        let vremePoljeB = document.createElement("input");
        vremePoljeB.className = "vremePolje2";
        vremePoljeB.type = "time";
        minikont2.appendChild(vremePoljeB);

        
        let labela3 = document.createElement("label");
        labela3.innerHTML="Sala:";
        minikont3.appendChild(labela3);

        let dugmeSale = document.createElement("button");
        dugmeSale.innerHTML=">";
        dugmeSale.className="dugmeSale";
        minikont3.appendChild(dugmeSale);
        dugmeSale.onclick=(ev)=>this.UveziSale2(minikont3);

        var salaSelect = document.createElement("select");
        salaSelect.className="salaSelect";
        minikont3.appendChild(salaSelect);

        
        let dugmeDodaj2 = document.createElement("button");
        dugmeDodaj2.innerHTML="DODAJ";
        dugmeDodaj2.className="Dugme";
        minikont4.appendChild(dugmeDodaj2);

        dugmeDodaj2.onclick=(ev)=>this.dodajPrezentovanje2(minikont1, minikont3, minikont2);
    }    

    UveziSale2(host) {

        this.listaSala=[];
        var selectSale= this.ObrisiPrethodneSale();
        
        let op;

        fetch("https://localhost:5001/Sajam/PreuzetiSale/"+this.id,
        {
            method:"GET"
        }).then(s=>{
            if(s.ok){
               
                s.json().then(data=>{
                    data.forEach(s=>{
                        let pr = new Sala(s.id,s.naziv);  

                        op=document.createElement("option");
                        op.innerHTML=s.naziv;
                        op.value=s.id;
                        selectSale.appendChild(op); 
                        this.listaSala.push(pr);

                    })


                })
            }
        })
    }

    ObrisiPrethodneSale()
    {
        let salaSelect = document.querySelector(".salaSelect");
        let roditelj = salaSelect.parentNode;
        roditelj.removeChild(salaSelect);

        salaSelect = document.createElement("select");
        salaSelect.className="salaSelect";
        roditelj.appendChild(salaSelect);
        return salaSelect;
    }

    
    dodajPrezentovanje2(minikont1, minikont3, minikont2){

        
        var datumEl = minikont2.querySelector(".datumPolje2");
        var datumSamo=datumEl.value;
        
        if(datumSamo===undefined || datumSamo===null || datumSamo==="")
        {
            alert("Unesite odgovarajući datum!");
        }
        var vremeEl = minikont2.querySelector(".vremePolje2");
        var vremeSamo=vremeEl.value;
        var datum = datumSamo+" "+vremeSamo;
        if(vremeSamo===undefined || vremeSamo===null || vremeSamo==="")
        {
            alert("Unesite odgovarajuće vreme!");
        }

        let optionEl = minikont1.querySelector("select");
        var kompanijaID = optionEl.options[optionEl.selectedIndex].value;
        
        let optionEl2 = minikont3.querySelector("select");        
        var salaID = optionEl2.options[optionEl2.selectedIndex].value;

        fetch("https://localhost:5001/Prezentovanje/DodatiPrezentovanje/"+this.id+"/"+salaID+"/"+kompanijaID+"/"+datum,
        {
            method:"POST"
        }).then( s=>
            {
                
                if(s.status == 400)
                {
                    alert("Dolazi do preklapanja datuma!");
                    return;
                }
                if (s.status == 200)
                {
                    
                    alert("Uspesno dodato prezentovanje!");
                    this.ucitajRaspored();
                    return;
                   
                }
            })


    }

    
}