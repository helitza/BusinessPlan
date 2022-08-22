//import { Mentor } from "./Mentor.js";


export class Task{
    
    constructor(id, pozicija, mentor){
        this.id = id;
        this.pozicija = pozicija;
        this.mentor = mentor;
    }

    crtajTaskRed(host){
        var tr=document.createElement("tr");
        host.appendChild(tr);

        var el = document.createElement("td");
        el.innerHTML=this.pozicija;
        tr.appendChild(el);

        el = document.createElement("td");
        el.innerHTML=this.mentor;
        tr.appendChild(el);

    }
    
}