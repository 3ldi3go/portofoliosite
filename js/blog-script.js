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
            const p = document.createElement("p");

            p.textContent = `${post.title} (${post.date}): ${post.content}`;

            li.appendChild(p);
            postMessages.appendChild(li);
        });
    
}

