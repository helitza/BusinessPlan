import { Task } from "./Task.js";
export class Kompanija{
    
    constructor(id, naziv, hr, oblast, osnivanje){
        this.id = id;
        this.naziv = naziv;
        this.hr=hr;
        this.oblast=oblast;
        this.osnivanje=osnivanje;
        this.kontejner=null;
        
    }

    crtajKompaniju(host)
    {
        var divKompanija = document.createElement("div");
        divKompanija.className="divKompanija";
        host.appendChild(divKompanija);
        this.kontejner = divKompanija;
        

        var el = document.createElement("div");
        el.className="naslovKompanije";
        el.innerHTML=this.naziv;
        divKompanija.appendChild(el);

        el = document.createElement("div");
        el.className="kompanijaPolje";
        el.innerHTML="HR: "+this.hr;
        divKompanija.appendChild(el);

        var OblastGodinaDiv = document.createElement("div");
        OblastGodinaDiv.className="OblastGodinaDiv";
        divKompanija.appendChild(OblastGodinaDiv);

        el = document.createElement("div");
        el.className="kompanijaPolje";
        el.innerHTML="oblast: "+this.oblast+" |    osnivanje: "+this.osnivanje;
        OblastGodinaDiv.appendChild(el);

       
        
        var taskovi = document.createElement("tbody");
        taskovi.className="divTaskovi";
        divKompanija.appendChild(taskovi);

        let dugme = document.createElement("button");
        dugme.className="dugmeTaskovi";
        dugme.innerHTML="TASKOVI";
        dugme.onclick=(ev)=>this.PrikaziTaskove();
        divKompanija.appendChild(dugme);

        let dugme2 = document.createElement("button");
        dugme2.className="dugmeTaskovi";
        dugme2.innerHTML="-";
        dugme2.onclick=(ev)=>this.ObrisiStareTaskove();
        divKompanija.appendChild(dugme2);


    }

    PrikaziTaskove()
    {

        fetch("https://localhost:5001/Kompanija/PreuzmiTaskoveKompanije/"+this.id,
        {
            method:"GET"
        }).then(s=>{

            var taskovi = this.ObrisiStareTaskove();
            if(s.ok){
               
                s.json().then(data=>{
            

                    data.forEach(s=>{
                        let pr = new Task(s.id,s.pozicija,s.mentor);                         
                        pr.crtajTaskRed(taskovi);
                    })
                })
            }

            
        })

    }

    ObrisiStareTaskove()
    {
        var taskovi = this.kontejner.querySelector(".divTaskovi");
        var roditelj = taskovi.parentNode;
        roditelj.removeChild(taskovi);
        taskovi = document.createElement("div");
        taskovi.className="divTaskovi";
        roditelj.appendChild(taskovi);  
        return taskovi;

    }
    

    
}