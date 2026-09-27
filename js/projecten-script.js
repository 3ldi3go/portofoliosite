const projecten = document.getElementById("projecten");
const projectenlijst = "../json/projecten.json";
const alfabetButton = document.getElementById("alfabet");


let projectenData = null;

fetch(projectenlijst)
    .then(response => response.json())
    .then(data => {
        projectenData = data.projects;
        maakProjectenLijst(projectenData);
    })  
    .catch(error => {
        console.error("Error fetching project data:", error);
    });
    

function maakProjectenLijst(projectenData) {
    projecten.innerHTML = "";
        projectenData.forEach(project => {
            const li = document.createElement("li");
            li.innerHTML = `
                <h3>${project.title}</h3>
                <p>${project.description}</p>
                <img src="${project.image}" alt="${project.title}">
            `;
            projecten.appendChild(li);
        });
}

alfabetButton.addEventListener("click", () => {
    const gesorteerdeProjecten = [...projectenData].sort((a, b) => a.title.localeCompare(b.title));
    maakProjectenLijst(gesorteerdeProjecten);
});

            
