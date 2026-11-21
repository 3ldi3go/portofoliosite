import Header from '../components/Header.jsx';
import NavBar from '../components/NavBar.jsx';
import JokeBanner from '../components/JokeBanner.jsx';
import ReferenceLinks from '../components/ReferenceLinks.jsx';
import BelangrijkLinks from '../components/BelangrijkLinks.jsx';
import '../css/home.css';

export default function Home() {
  return (
    <div className="page-home">
      <Header title="Welkom op mijn Home Pagina">
        <JokeBanner />
      </Header>

      <NavBar variant="home" />

      <main>
        <div className="item">
          <div className="box">
            <h2>Over deze website:</h2>
            <p>
              Deze website is een portfolio website waar ik mijn projecten en blog kan laten zien.
              De website is gemaakt met HTML, CSS en JavaScript. <br />
              De website is gemaakt voor mijn studie Software Engineering aan de Haagse Hogeschool.
              <br /><br />
              Het leek mij best handig om er een home pagina aan toe te voegen. Wat ik hier nog meer
              zet weet ik nog niet, maar alle belangrijke informatie staat op de andere pagina's.
            </p>
          </div>
          <ReferenceLinks />
        </div>

        <BelangrijkLinks />
      </main>
    </div>
  );
}
