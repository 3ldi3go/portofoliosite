export default function ProjectCard({ project }) {
  return (
    <li>
      <p>
        <strong>{project.title}</strong>
        <br />
        {project.description}
      </p>
      {project.image && <img src={project.image} alt={project.title} />}
    </li>
  );
}
