import {Hotel} from "./hotel.js";
import { Creds } from "./creds.js"

//const hotelProba = new Hotel(1,"Hotel Proba", "Pr123",5);
//console.log(hotelProba);
//hotelProba.crtajHotel(document.body);

let main = document.createElement("div");
main.className = "bodyMain";
document.body.appendChild(main);
let subBody = document.createElement("div");
document.body.appendChild(subBody);

let creds = new Creds();
let baseUrl = creds.getUrl();

//Dodavanje hotela
let kontejnerDodaj = document.createElement("div");
kontejnerDodaj.className = "kontUpravljanje";
main.appendChild(kontejnerDodaj);


function _fetch(url, options){
	let opt = { 
			headers: {
				"Access-Control-Allow-Credentials": true, 
				"Access-Control-Allow-Headers": "Content-Type", 
				"Access-Control-Allow-Methods": "DELETE,GET,HEAD,OPTIONS,PATCH,POST,PUT", 
				"Access-Control-Allow-Origin": "*" 
			}  
		};
		
	if (options!=undefined) opt = options;
	return fetch(baseUrl+url, opt);
}

function _dodajSobeZaHotel(hotelId,brSoba){
    // hotelID - id hotela u kojem ce se zadati sobe
    // brSoba - broj soba za svaki od tipova soba 
    // (ukupan broj soba je brSoba*4, jer postoji 4 razlicite vrste soba)
    
    for(let i=0;i<4;i++)
    {
        for(let j=0;j<brSoba;j++)
        {
            let roomNumber = (i*brSoba)+j+1;
            // Dodavanje nove sobe u hotel sa zadatim ID-jem
            _fetch("/Soba/"+ hotelId,{
                method:"POST",
                headers:{
                    "Content-Type":"application/json"
                },
                body: JSON.stringify({
                brojSobe:roomNumber,
                brojKreveta:i+1,
                trenutniKapacitet:0,
                brojDana:0,
                gost:"",
                zauzeta:false,
                sredjivanje:false
                })
            });
        }
    }
}

function _dodajRacuneZaHotel(hotelId,brSoba){
    // hotelID - id hotela u kojem ce se zadati sobe
    // brSoba - broj soba za svaki od tipova soba 
    // (ukupan broj soba je brSoba*4, jer postoji 4 razlicite vrste soba)

    for(let i=0;i<4;i++)
    {
        for(let j=0;j<brSoba;j++)
        {
            let roomNumber = (i*brSoba)+j+1;

            let tip;
            if(i==0)
                tip = "Jednokrevetna soba";
            else if(i==1)
                tip = "Dvokrevetna soba";
            else if(i==2)
                tip = "Trokrevetna soba";
            else 
                tip = "Cetvorokrevetna soba";

            // Kreiranje racuna za svaku od soba u hotelu sa prosledjenim ID-jem
            _fetch("/Racun/"+ hotelId,{
                method:"POST",
                headers:{
                    "Content-Type":"application/json"
                },
                body: JSON.stringify({
                brojSobe:roomNumber,
                tipSobe:tip,
                brojDana:0,
                roomService:false,
                spaCentar:false
                })
            });
        }
    }
}

let elLabela = document.createElement("h3");
elLabela.innerHTML = "Dodaj novi hotel";
kontejnerDodaj.appendChild(elLabela);
let elInput;
let podaci = ["Naziv:","Adresa:","Broj soba po tipu:"];
let klase = ["nazivHotela", "adresaHotela", "brojSobaUHotelu"];
let tipovi = ["text","text","number"];
podaci.forEach((el, ind) =>
{
    elLabela = document.createElement("label");
    elLabela.innerHTML = el;
    kontejnerDodaj.appendChild(elLabela);
    elInput = document.createElement("input");
    elInput.type = tipovi[ind];
    elInput.className = klase[ind];
    kontejnerDodaj.appendChild(elInput);
})
kontejnerDodaj.appendChild(document.createElement("br"));
const dugmeDodaj = document.createElement("button");
dugmeDodaj.innerHTML="Dodaj hotel";
dugmeDodaj.className = "dugmici";
kontejnerDodaj.appendChild(dugmeDodaj);

dugmeDodaj.onclick=(ev)=>{

    let nazivH = kontejnerDodaj.querySelector(".nazivHotela").value;
    let adresaH = kontejnerDodaj.querySelector(".adresaHotela").value;
    let brojH = kontejnerDodaj.querySelector(".brojSobaUHotelu").value;

    if (nazivH==undefined || nazivH.trim()=="") {
        alert("Naziv hotela ne moze da bude prazan!");
        return;
    }

    if (brojH==undefined || isNaN(brojH) || (!isNaN(brojH) && brojH<1)) {
        alert("Broj soba u hotelu neispravan!");
        return;
    }

    // Kreiranje novog hotela
    _fetch("/Hotel", {
        method:"POST",
        headers:{
            "Content-Type":"application/json"
        },
        body: JSON.stringify({
            naziv:nazivH,
            adresa:adresaH,
            brojSobaPoTipu:brojH
        })
    }).then(p=>{
        // Po uspesnom kreiranju hotela, dobicemo odgovor koji predstavlja
        // entitet hotela u bazi (sa njegovim pravim ID-jem, i ostalim poljima).
        // Podatke iz tog entiteta mozemo iskoristiti za kreiranje soba i racuna
        // za zadati hotel.
        p.json().then(data=>{
            let brSoba = data.brojSobaPoTipu;
            let hID = data.id;
            _dodajSobeZaHotel(hID,brSoba);
            _dodajRacuneZaHotel(hID,brSoba);
        });
    });
    
}
kontejnerDodaj.appendChild(document.createElement("br"));
kontejnerDodaj.appendChild(document.createElement("br"));

//Crtanje svih hotela

let kontejnerCrtajSve = document.createElement("div");
kontejnerCrtajSve.className = "kontUpravljanje";
main.appendChild(kontejnerCrtajSve);

elLabela = document.createElement("h3")
elLabela.innerHTML = "Crtanje hotela";
kontejnerCrtajSve.appendChild(elLabela);

//Brisanje hotela
let kontejnerBrisanje = document.createElement("div");
kontejnerBrisanje.className = "kontUpravljanje";
main.appendChild(kontejnerBrisanje);

elLabela = document.createElement("h3");
elLabela.innerHTML = "Brisanje hotela";
kontejnerBrisanje.appendChild(elLabela);
elLabela = document.createElement("label");
elLabela.innerHTML = "ID hotela:";
kontejnerBrisanje.appendChild(elLabela);

elInput = document.createElement("input");
elInput.className ="idBrisanjeHotela";
elInput.type ="number";
kontejnerBrisanje.appendChild(elInput);

kontejnerBrisanje.appendChild(document.createElement("br"));

let dugmeBrisanje = document.createElement("button");
dugmeBrisanje.innerHTML="Obrisi hotel";
dugmeBrisanje.className = "dugmici";
kontejnerBrisanje.appendChild(dugmeBrisanje);
dugmeBrisanje.onclick = (ev) =>
{
    let id = document.querySelector(".idBrisanjeHotela").value;
    
    // Uklanjanje hotela sa zadatim ID-jem
    _fetch("/Hotel?id="+id,{method:"DELETE"}).then( p=> {
        if (p.ok) 
        {
            location.reload();
        }
        else{
            alert("Zadati hotel sa ID-jem: "+ id +" ne postoji.");
        }
    });
}


const dugmeCrtaj = document.createElement("button");
dugmeCrtaj.innerHTML="Crtaj sve hotele";
dugmeCrtaj.className = "dugmici";
kontejnerCrtajSve.appendChild(dugmeCrtaj);

dugmeCrtaj.onclick = (ev) => {


   // Pribavljanje svih hotela
   // Method GET je podrazumevan
   _fetch("/Hotel").then( p=> {
        p.json().then(data => {
            subBody.innerHTML='';
            data.forEach(hotel => {
                console.log(hotel);
                const hotel1 = new Hotel(hotel.id, hotel.naziv, hotel.adresa, hotel.brojSobaPoTipu);
                hotel1.crtajHotel(subBody);
                hotel1.sobe.forEach(soba=>{
                    let s = soba;
                    s.azurirajSobu(soba.gost,soba.trenutniKapacitet,soba.brojDana);
                });
                hotel1.racuni.forEach(racun=> {
                    let r = racun;
                    r.azurirajRacun(racun.tip, racun.brojDana, racun.roomService, racun.spaCentar);
                });
    
            });
        });
    });

}