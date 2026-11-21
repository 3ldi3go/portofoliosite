import Header from '../components/Header.jsx';
import NavBar from '../components/NavBar.jsx';
import InfoItem from '../components/InfoItem.jsx';
import '../css/whoami.css';

const HOBBIES = ['Gamen', 'Tekenen', 'Programmeren', 'Horror films kijken'];
const SKILLS = ['HTML', 'CSS', 'JavaScript', 'C#', 'Java'];

export default function WhoAmI() {
  return (
    <div className="page-whoami">
      <Header title="WhoAmI" />
      <NavBar />

      <main>
        <InfoItem imgSrc="frontend-react\img\diego.jpeg" imgAlt="foto van mijzelf">
          <h2>Over mij:</h2>
          <p className="beschrijfing">
            Mijn naam is Diego van Hagen. Ik ben 20 jaar oud en studeer momenteel aan de Haagse
            HogeSchool. Ik ben iemand die best wel eens nieuwe dingen wilt leren en plezier haalt uit
            het coderen, verder ben ik zeer creatief en vind ik mezelf best ambitieus en wil altijd
            mijn best doen om iets voor elkaar te kunnen
            <br /><br />
            Natuurlijk heb ik zoals iedereen hobbies en die zijn als volgt:
          </p>
          <ul className="hobbies">
            {HOBBIES.map((h) => (
              <li key={h}><em>{h}</em></li>
            ))}
          </ul>
        </InfoItem>

        <InfoItem imgSrc="frontend-react\img\error.png" imgAlt="placeholder">
          <h2>Mijn ervaring:</h2>
          <p className="beschrijfing">Ik heb redelijke ervaring met een paar programmeertalen, zoals:</p>
          <ul className="skills">
            {SKILLS.map((s) => (
              <li key={s}>{s}</li>
            ))}
          </ul>
          <p className="beschrijfing">Iets over mijn skills noteren nog denk hierbij aan: creatief</p>
          <p className="slottekst">
            Hopelijk heb ik hiermee kort een goed beeld over mezelf kunnen schetsen en ik hoop dat u
            als bezoeker plezier zal vinden in het bekijken van mijn website. <br />
            Geniet ervan :3
          </p>
          <p className="slottekst">
            Groetjes, <br />
            Diego
          </p>
        </InfoItem>
      </main>
    </div>
  );
}
