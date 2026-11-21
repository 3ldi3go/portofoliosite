const REFERENCES = [
  { href: 'https://github.com/3ldi3go', img: 'frontend-react/img/github.png', alt: 'GitHub Profile' },
  { href: 'https://www.linkedin.com/in/diego-van-hagen-594b29432/', img: 'frontend-react/img/linkedin.png', alt: 'LinkedIn Profile' },
];

export default function ReferenceLinks() {
  return (
    <nav className="references">
      {REFERENCES.map((r) => (
        <a key={r.href} href={r.href} target="_blank" rel="noopener noreferrer">
          <img src={r.img} alt={r.alt} />
        </a>
      ))}
    </nav>
  );
}
