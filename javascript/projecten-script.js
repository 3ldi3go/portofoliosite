const projecten = document.getElementById("projecten");

const projectData = [
    {
        naam: "Smart Telefoon tas",
        beschrijving: "Dit was mijn project van het eerste semester." +
        " Hierbij krijgen we een willekeurige SDG en die van ons kwam op school terecht. We hadden besloten om een slimme telefoon tas te ontwerpen die kan controleren of een specifieke leerling zijn telefoon in de tas heeft gezet of anders dus nog bij zichzelf heeft." +
        "Dit was voor ons toen een manier om de algehele telefoonverbod tegen te gaan en het voor docenten makkelijker te maken om te controleren of leerlingen hun telefoons echt weg deden",
        afbeelding: "../img/cat-mlem.png"
    },
    {
        naam: "Hotel Simulatie",
        beschrijving: "Een simpele simulatie van een hotel, waarmee het einddoel is om de werking van een hotel te simuleren in bepaalde situaties." +
            "Stel dus dat het druk is en alles goed verloopt en er geen verstoppingen komen. Dat liften goed werken en wat er gebeurd in het geval van een emergency.",
        afbeelding: "../img/hotelSimulatie.png"
    },
    {
        naam: "Project 3",
        beschrijving: "Beschrijving van project 3",
        afbeelding: "../img/cat-mlem.png"
    }
]

projectData.forEach(project => {
    const li = document.createElement("li");