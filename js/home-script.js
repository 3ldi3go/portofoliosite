const url = "https://v2.jokeapi.dev/joke/Programming,Pun,Spooky?blacklistFlags=nsfw,racist,sexist";
const joke = document.getElementById("joke");

fetch(url)
    .then(response => {
        if (!response.ok) {
            throw new Error(`Server error: ${response.status}`);
        }
        return response.json();
    })
    .then(data => {
        joke.innerHTML = ""; // Clear any existing content
        const jokeElement = document.getElementById("joke");
        if (data.type === "single") {
            jokeElement.textContent = data.joke;
        } else if (data.type === "twopart") {
            jokeElement.textContent = `${data.setup} - ${data.delivery}`;
        }
        joke.appendChild(jokeElement);
    })
    .catch(error => {
        console.error("Error fetching joke data:", error);
    });
