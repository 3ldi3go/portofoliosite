const form = document.getElementById("contactForm");

const velden = [
    {id : "naam", boodschap: "Vul minimaal twee tekens in"},
    {id : "email", boodschap: "Vul een geldig e-mailadres in"},
    {id : "bericht", boodschap: "Vul minimaal tien tekens in"}
]

function validatie(veld) {
    const input = document.getElementById(veld.id);
    const foutmelding = document.getElementById(`${veld.id}Error`);
    const geldig = input.checkValidity();

    input.setAttribute("aria-invalid", String(!geldig));
    foutmelding.textContent = geldig ? "" : veld.boodschap;

    return geldig;
}


form.addEventListener("submit", function(event) {
    event.preventDefault();
    
    const allesGeldig = velden.map(validatie).every(Boolean);
    const status = document.getElementById("formstatus");

    if (allesGeldig) {
        status.textContent = "Formulier succesvol verzonden!";
        form.reset();
    } else {
        status.textContent = "Er zijn fouten in het formulier. Controleer de velden.";
        return;
    }
});

