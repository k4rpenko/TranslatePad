import styles from '../styles/home.module.css';

export default function Home() {
  return (
    <main className="flex min-h-screen flex-col items-center justify-between p-24">
      <div className={styles.Start}>
        <div className={styles.intro}>
          <div className={styles.title}>Translate-pad: A new Words? Make ur life easyer.</div>
          <div className={styles.subtitle}>Your complete All-in-One solution for to combine a dictionary and notes. Build awesome notes and fast Add words with translations to the dictionary.</div>
          <div className={styles.links}>
            <button className="btn">Download</button>
          </div>
        </div>
        <div className={styles.gradientBackground}></div>
        <div className={styles.notifications}>
          <div className={styles.notificationsOne}>
            <div>У нас повністю відкритий код, який ви можете переглянути в GiHub.</div>
          </div>
          <div className={styles.notificationTwo}>
            <div>Payment received · 15m ago</div>
          </div>
          <div className={styles.notificationThre}>
            <div>Easy Deploy · 2m ago</div>
          </div>
        </div>
      </div>
    </main>
  );
}
