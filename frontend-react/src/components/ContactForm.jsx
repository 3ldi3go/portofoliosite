import { useState } from 'react';

const validate = {
  naam: (v) => (v.trim().length < 2 ? 'Vul een naam in van minimaal 2 tekens.' : ''),
  email: (v) => (/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(v) ? '' : 'Vul een geldig e-mailadres in.'),
  bericht: (v) => (v.trim().length < 10 ? 'Je bericht moet minimaal 10 tekens bevatten.' : ''),
};

const EMPTY = { naam: '', email: '', bericht: '' };

export default function ContactForm() {
  const [values, setValues] = useState(EMPTY);
  const [errors, setErrors] = useState(EMPTY);
  const [status, setStatus] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setValues((v) => ({ ...v, [name]: value }));
    if (errors[name]) setErrors((er) => ({ ...er, [name]: validate[name](value) }));
  };

  const handleBlur = (e) => {
    const { name, value } = e.target;
    setErrors((er) => ({ ...er, [name]: validate[name](value) }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const newErrors = Object.fromEntries(Object.keys(validate).map((k) => [k, validate[k](values[k])]));
    setErrors(newErrors);
    if (Object.values(newErrors).some(Boolean)) {
      setStatus('Controleer de gemarkeerde velden.');
      return;
    }
    // TODO: send `values` to your backend here (fetch/axios)
    setStatus('Bedankt voor je bericht!');
    setValues(EMPTY);
  };

  const field = (name) => ({
    id: name,
    name,
    value: values[name],
    onChange: handleChange,
    onBlur: handleBlur,
    required: true,
    'aria-describedby': `${name}Error`,
    'aria-invalid': errors[name] ? 'true' : 'false',
  });

  return (
    <form id="contactForm" onSubmit={handleSubmit} noValidate>
      <label htmlFor="naam">Naam:</label>
      <input type="text" {...field('naam')} />
      <span id="naamError" className="error-message" role="alert">{errors.naam}</span>

      <label htmlFor="email">E-mail:</label>
      <input type="email" {...field('email')} />
      <span id="emailError" className="error-message" role="alert">{errors.email}</span>

      <label htmlFor="bericht">Bericht:</label>
      <textarea {...field('bericht')} />
      <span id="berichtError" className="error-message" role="alert">{errors.bericht}</span>

      <button type="submit">Verzenden</button>
      <div id="formstatus" aria-live="polite">{status}</div>
    </form>
  );
}
