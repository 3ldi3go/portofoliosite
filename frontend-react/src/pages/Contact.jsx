import Header from '../components/Header.jsx';
import NavBar from '../components/NavBar.jsx';
import ContactForm from '../components/ContactForm.jsx';
import '../css/contact.css';

export default function Contact() {
  return (
    <div className="page-contact">
      <Header title="Contactformulier" />
      <NavBar />
      <main>
        <ContactForm />
      </main>
    </div>
  );
}
