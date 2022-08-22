export class Prezentovanje{
    
    constructor(id, datum, vreme, sala, kompanija){
        this.id = id;
        this.datum = datum;
        this.vreme=vreme;
        this.sala= sala;
        this.kompanija = kompanija;
    }

    crtajPrezentovanjeRed(host){
        var tr=document.createElement("tr");
        host.appendChild(tr);

        var elDatum = document.createElement("td");
        elDatum.className="tabelaDatum";
        elDatum.innerHTML=this.datum;
        tr.appendChild(elDatum);

        var elVreme = document.createElement("td");
        elVreme.className="tabelaVreme";
        elVreme.innerHTML=this.vreme;
        tr.appendChild(elVreme);

        var elSala = document.createElement("td");
        elSala.className="tabelaSala";
        elSala.innerHTML=this.sala;
        tr.appendChild(elSala);

        var elKompanija = document.createElement("td");
        elKompanija.className="tabelaKompanija"
        elKompanija.innerHTML=this.kompanija;
        tr.appendChild(elKompanija);

        var elObrisi = document.createElement("td");
        tr.appendChild(elObrisi);

        var dugmeObrisi = document.createElement("button");
        dugmeObrisi.innerHTML="OBRIŠI";
        dugmeObrisi.className="dugmeObrisi";
        elObrisi.appendChild(dugmeObrisi);

        dugmeObrisi.onclick=(ev)=>this.ObrisiPrezentovanje(tr);
        

        var dugmeIzmeni2 = document.createElement("button");
        dugmeIzmeni2.innerHTML="IZMENI";
        dugmeIzmeni2.className="dugmeIzmeni";
        elObrisi.appendChild(dugmeIzmeni2);
        dugmeIzmeni2.onclick=(ev)=>this.IzmeniPrezentovanje2(tr);
        
        
    }

    ObrisiPrezentovanje(host){

        fetch("https://localhost:5001/Prezentovanje/ObrisatiPrezentovanje/"+this.id,
        {
            method:"DELETE"
        }).then(s =>
        {
            if(s.status == 400)
            {
                alert("Greška!");
                return;
            }
            if(s.ok)
            {
               alert("Uspešno obrisano prezentovanje!");
               this.ObrisiIzTabele(host); 

            }
            
        })
    }

    ObrisiIzTabele(host){

        let roditelj = host.parentNode;
        roditelj.removeChild(host);
    }

    

    IzmeniPrezentovanje2(tr)
    {
        var datumEl = document.querySelector(".datumPolje2");
        var datumSamo=datumEl.value;
        console.log(datumSamo);

        var vremeEl = document.querySelector(".vremePolje2");
        var vremeSamo=vremeEl.value;
        console.log(vremeSamo);

        var datum = datumSamo+" "+vremeSamo;
        console.log(datum);

        let optionElKompanija = document.querySelector(".kompanijaSelect");
        var kompanijaID = optionElKompanija.options[optionElKompanija.selectedIndex].value;
        let optionEl = document.querySelector(".salaSelect");
        var salaID = optionEl.options[optionEl.selectedIndex].value;
        let optionElSaj = document.querySelector(".selectSajam");
        var sajamID = optionElSaj.options[optionElSaj.selectedIndex].value;
        

        
        fetch("https://localhost:5001/Prezentovanje/IzmeniPrezentovanje/"+this.id+"/"+sajamID+"/"+salaID+"/"+kompanijaID+"/"+datum,
        {
            method:"PUT"
        }).then(s=>{
            if(s.status == 200)
                {
                    alert("Izmenjeno prezentovanje!");
                    this.RefreshPrezentovanje(tr);
                    return;
                }
        })
    }

    RefreshPrezentovanje(tr)
    {
        var roditelj=tr.parentNode;
        this.ObrisiIzTabele(tr);
        this.crtajPrezentovanjeRed(roditelj);
    }



}