import { useRef, useState } from 'react';

// Accordion item: button toggles the paragraph (animated via max-height, see blog.css)
export default function BlogPost({ post }) {
  const bodyRef = useRef(null);
  const [open, setOpen] = useState(false);

  const style = open
    ? { maxHeight: (bodyRef.current?.scrollHeight ?? 0) + 30, paddingTop: 12, paddingBottom: 12 }
    : undefined;

  return (
    <li>
      <button type="button" aria-expanded={open} onClick={() => setOpen((o) => !o)}>
        {post.title}
      </button>
      <p ref={bodyRef} style={style}>
        {post.content}
      </p>
    </li>
  );
}
