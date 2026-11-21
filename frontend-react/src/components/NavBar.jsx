import { Link, useLocation } from 'react-router-dom';

const LINKS = [
  { to: '/', className: 'home', label: 'Terug naar Home' },
  { to: '/whoami', className: 'wieBenIk', label: 'WhoAmI' },
  { to: '/projecten', className: 'projecten', label: 'Projecten' },
  { to: '/blog', className: 'blog', label: 'Mijn blog' },
  { to: '/contact', className: 'contact', label: 'Contactformulier' },
];

/**
 * Main navigation.
 * - variant "home": the bar used on the home page (class "basic", no "Terug naar Home" link)
 * - default: the bar on the other pages (hides the link to the current page, like the original HTML)
 */
export default function NavBar({ variant }) {
  const { pathname } = useLocation();
  const isHome = variant === 'home';
  const links = LINKS.filter((l) => (isHome ? l.to !== '/' : l.to !== pathname));

  return (
    <nav className="navbar">
      {links.map((l) => (
        <Link key={l.to} to={l.to} className={l.className}>
          {l.label}
        </Link>
      ))}
    </nav>
  );
}