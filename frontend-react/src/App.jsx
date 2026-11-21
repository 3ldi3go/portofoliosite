import { Routes, Route } from 'react-router-dom';
import Home from './pages/Home.jsx';
import WhoAmI from './pages/WhoAmI.jsx';
import Projecten from './pages/Projecten.jsx';
import Blog from './pages/Blog.jsx';
import Contact from './pages/Contact.jsx';

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/whoami" element={<WhoAmI />} />
      <Route path="/projecten" element={<Projecten />} />
      <Route path="/blog" element={<Blog />} />
      <Route path="/contact" element={<Contact />} />
    </Routes>
  );
}
