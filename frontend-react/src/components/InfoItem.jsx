// A white box with text next to an image (used on the WhoAmI page)
export default function InfoItem({ imgSrc, imgAlt, children }) {
  return (
    <div className="item">
      <div className="box">{children}</div>
      <img src={imgSrc} alt={imgAlt} />
    </div>
  );
}
