import React from "react";
import table from "./table.module.css";
import { DirectionType, RoundType } from "../../types/types";
import leftArrow from "../../images/right-arrow (2).png";
import rightArrow from "../../images/right-arrow (1).png";
import { FORWARD, BACKWARD, ENCRYPTION_MODE } from "../../constants/constants";
import { useSelector } from "../../services/hooks";

function Table() {
  const [shownRounds, setShownRounds] = React.useState<any[]>([]);
  const [page, setPage] = React.useState<number>(0);
  const result = useSelector((state) => state.encryption.result);
  const mode = useSelector((state) => state.encryption.mode);

  React.useEffect(() => {
    let tempArray: any[] = [];
    let newShownRounds: any[][] = [];

    result?.rounds.forEach((round: RoundType, index: number) => {
      tempArray.push(round);

      if ((index + 1) % 4 === 0) {
        newShownRounds.push([...tempArray]);
        tempArray = [];
      }
    });

    if (tempArray.length > 0) {
      newShownRounds.push(tempArray);
    }

    setShownRounds(newShownRounds);
  }, [result]);

  function onPageChange(direction: DirectionType) {
    switch (direction) {
      case FORWARD:
        setPage((prevState) => prevState + 1);
        break;
      case BACKWARD:
        setPage((prevState) => prevState - 1);
        break;
      default:
        break;
    }
  }

  return (
    <section className={table.section}>
      <div
        className={`${table.buttonSection} ${
          page === 0 && table.buttonSectionHidden
        }`}
      >
        <button
          onClick={() => onPageChange(BACKWARD)}
          type="button"
          className={table.button}
        >
          <img src={leftArrow} alt="left arrow icon" className={table.image} />
        </button>
        <p>
          Rounds&nbsp;
          {shownRounds.length > 0 &&
            page !== 0 &&
            shownRounds[page - 1][0].roundNumber}
          &nbsp;-&nbsp;
          {shownRounds.length > 0 &&
            page !== 0 &&
            shownRounds[page - 1][shownRounds[page - 1].length - 1].roundNumber}
        </p>
      </div>

      <div className={table.tables}>
        <table className={table.table}>
          <tbody>
            <tr>
              <td colSpan={2}>
                {mode === ENCRYPTION_MODE ? "plain text" : "cipher text"}
              </td>
              <td colSpan={2}>{result?.plainText}</td>
            </tr>
            <tr>
              <td colSpan={2}>initial whitening key</td>
              <td colSpan={2}>{result?.initialWhiteningKey}</td>
            </tr>
            <tr>
              <td colSpan={2}>after whitening</td>
              <td colSpan={2}>{result?.afterInitialWhitening}</td>
            </tr>
          </tbody>
        </table>
        <div className={table.roundColumns}>
          {shownRounds.length > 0 &&
            shownRounds[page].map((round: any, indexOfRound: number) => {
              return (
                <table className={table.table} key={indexOfRound}>
                  <thead>
                    <tr>
                      <th colSpan={4}>Round {round.roundNumber}</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td colSpan={2}>input</td>
                      <td colSpan={2}>{round.input}</td>
                    </tr>
                    <tr>
                      <td>F-function</td>
                      <td colSpan={2}>F0</td>
                      <td>F1</td>
                    </tr>
                    <tr>
                      <td>input</td>
                      <td colSpan={2}>{round.input.split(" ")[0]}</td>
                      <td>{round.input.split(" ")[2]}</td>
                    </tr>
                    <tr>
                      <td>round key</td>
                      <td colSpan={2}>{round.roundKey.split(" ")[0]}</td>
                      <td>{round.roundKey.split(" ")[1]}</td>
                    </tr>
                    <tr>
                      <td>after key add</td>
                      <td colSpan={2}>{round.afterKeyAdd.split(" ")[0]}</td>
                      <td>{round.afterKeyAdd.split(" ")[1]}</td>
                    </tr>
                    <tr>
                      <td>after S</td>
                      <td colSpan={2}>{round.afterS.split(" ")[0]}</td>
                      <td>{round.afterS.split(" ")[1]}</td>
                    </tr>
                    <tr>
                      <td>after M</td>
                      <td colSpan={2}>{round.afterM.split(" ")[0]}</td>
                      <td>{round.afterM.split(" ")[1]}</td>
                    </tr>
                  </tbody>
                </table>
              );
            })}
        </div>
        {page === shownRounds.length - 1 && (
          <table className={table.table}>
            <tbody>
              <tr>
                <td colSpan={2}>output</td>
                <td colSpan={2}>{result?.output}</td>
              </tr>
              <tr>
                <td colSpan={2}>final whitening key</td>
                <td colSpan={2}>{result?.finalWhiteningKey}</td>
              </tr>
              <tr>
                <td colSpan={2}>after whitening</td>
                <td colSpan={2}>{result?.afterFinalWhitening}</td>
              </tr>
              <tr>
                <td colSpan={2}>
                  {mode === ENCRYPTION_MODE ? "cipher text" : "plain text"}
                </td>
                <td colSpan={2}>{result?.cipherText}</td>
              </tr>
            </tbody>
          </table>
        )}
      </div>
      <div
        className={`${table.buttonSection} ${
          page === shownRounds.length - 1 && table.buttonSectionHidden
        }`}
      >
        <button
          onClick={() => onPageChange(FORWARD)}
          type="button"
          className={table.button}
        >
          <img
            src={rightArrow}
            alt="right arrow icon"
            className={table.image}
          />
        </button>
        <p>
          Rounds&nbsp;
          {shownRounds.length > 0 &&
            page !== shownRounds.length - 1 &&
            shownRounds[page + 1][0].roundNumber}
          &nbsp;-&nbsp;
          {shownRounds.length > 0 &&
            page !== shownRounds.length - 1 &&
            shownRounds[page + 1][shownRounds[page + 1].length - 1].roundNumber}
        </p>
      </div>
    </section>
  );
}

export default Table;
