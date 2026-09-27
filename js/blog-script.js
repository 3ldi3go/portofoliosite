const postMessages = document.getElementById("blogPosts");
const blogPostsList = "../json/posts.json";

let blogPostsData = null;

fetch(blogPostsList)
    .then(response => response.json())
    .then(data => {
        blogPostsData = data.posts;
        maakBlogPostsLijst(blogPostsData);})
    .catch(error => {
        console.error("Error fetching blog data:", error);
    });

function maakBlogPostsLijst(blogPostsData){
    postMessages.innerHTML = "";

    blogPostsData.forEach(post => {
        const li = document.createElement("li");
        const button = document.createElement("button");
        const p = document.createElement("p");

        button.classList.add("collapsible");
        button.textContent = `${post.title} (${post.date})`;

        p.textContent = post.content;

        li.appendChild(button);
        li.appendChild(p);
        postMessages.appendChild(li);
    });

    document.querySelectorAll(".collapsible").forEach(button => {
        button.addEventListener("click", function() {
            this.classList.toggle("active");
            const p = this.nextElementSibling;

            if (p.style.maxHeight) {
                p.style.maxHeight = null;
                p.style.padding = "0 20px";
            } else {
                p.style.padding = "20px";
                p.style.maxHeight = p.scrollHeight +  "px";
            }
        });
    });
}

