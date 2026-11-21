import { useMemo, useState } from 'react';
import Header from '../components/Header.jsx';
import NavBar from '../components/NavBar.jsx';
import ProjectCard from '../components/ProjectCard.jsx';
import projects from '../data/projects.js';
import '../css/projecten.css';

export default function Projecten() {
  const [sorted, setSorted] = useState(false);

  const list = useMemo(
    () => (sorted ? [...projects].sort((a, b) => a.title.localeCompare(b.title, 'nl')) : projects),
    [sorted]
  );

  return (
    <div className="page-projecten">
      <Header title="Projecten" />
      <NavBar />

      <main>
        <h2>Een lijst van alle projecten waar ik aan gewerkt heb:</h2>
        <button id="alfabet" type="button" onClick={() => setSorted((s) => !s)}>
          {sorted ? 'Originele volgorde' : 'Op alfabetische volgorde'}
        </button>
        <ul id="projecten">
          {list.map((p) => (
            <ProjectCard key={p.id} project={p} />
          ))}
        </ul>
      </main>
    </div>
  );
}
