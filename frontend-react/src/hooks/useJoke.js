import { useEffect, useState } from 'react';

// Placeholder: swap the URL/parsing for whatever your home-script.js used.
export default function useJoke() {
  const [joke, setJoke] = useState('');

  useEffect(() => {
    let cancelled = false;
    fetch('https://v2.jokeapi.dev/joke/Programming,Dark,Pun,Spooky?blacklistFlags=nsfw,racist,sexist&type=single')
      .then((r) => r.json())
      .then((d) => !cancelled && setJoke(`${d.joke}`))
      .catch(() => {});
    return () => { cancelled = true; };
  }, []);

  return joke;
}
