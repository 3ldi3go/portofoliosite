import useJoke from '../hooks/useJoke.js';

// The <p id="joke"> in the home header
export default function JokeBanner() {
  const joke = useJoke();
  return <p id="joke">{joke}</p>;
}
