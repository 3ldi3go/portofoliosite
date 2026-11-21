import { Link } from 'react-router-dom';

// The three big image tiles on the home page
export default function BelangrijkLinks() {
  return (
    <nav className="belangrijk">
      <Link className="wieBenIk" to="/whoami">Wie ben ik en wat kan ik?</Link>
      <Link className="projecten" to="/projecten">Waar heb ik aan gewerkt?</Link>
      <Link className="blog" to="/blog">Mijn leuke blog waar ik update waar ik mee bezig ben</Link>
    </nav>
  );
}
