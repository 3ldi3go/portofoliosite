import Header from '../components/Header.jsx';
import NavBar from '../components/NavBar.jsx';
import BlogPost from '../components/BlogPost.jsx';
import posts from '../data/posts.js';
import '../css/blog.css';

export default function Blog() {
  return (
    <div className="page-blog">
      <Header title="Welkom op mijn eigen Blog" />
      <NavBar />

      <main>
        <ul id="blogPosts">
          {posts.map((p) => (
            <BlogPost key={p.id} post={p} />
          ))}
        </ul>
      </main>
    </div>
  );
}
