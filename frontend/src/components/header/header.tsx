import React from 'react';
import header from './header.module.css';

function Header() {
  return (
    <header className={header.header}>
        <h1 className={header.title}>CLEFIA Algorithm</h1>
    </header>
  );
}

export default Header;