"use client"
import Link from "next/link";
import styles from '../styles/Header.module.css';
import { useEffect, useState } from "react";


export default function Right() {
    const [Auth, setAuth] = useState(false);
    const [Avatar, setAvatar] = useState("");
    const [Email, setEmail] = useState("");

    useEffect(() => {
      const fetchTopics = async () => {
        try { 
          const res = await fetch('/api/auth/Login', {
            method: 'GET',
            credentials: 'include',
            cache: 'no-store',
          });
          if (res.ok) {
            const data = await res.json();
            setAvatar(data.Avatar || '');
            setEmail(data.Email || '');
            setAuth(true);
          } else if (res.status === 400) {
            console.error('Failed to fetch topics');
            window.location.href = '/';
          }
        } catch (error) {
          console.error('Error during topic fetch:', error);
        }
      };
      
      fetchTopics();
    }, []);

    return(
        <div className={styles.Right}>
            {Auth ? (
                <div>
                    <img src={Avatar} alt="Avatar" />
                    <p>{Email}</p>
                </div>
            ) : (
                <div>
                    <Link href="/login" className={styles.Login}>Login</Link>
                    <Link href="/register" className={styles.Sign}>Sign up</Link>
                </div>
            )}
        </div>
    )
}

