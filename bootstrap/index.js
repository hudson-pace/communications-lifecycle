const divisors = ['1','2','3','4','6','12'];
const container = document.querySelector('#main-container');
divisors.forEach((divisor) => {
  const row = document.createElement('div');
  row.classList.add('row');
  for (let i = 0; i < 12 / divisor; i++) {
    const col = document.createElement('div');
    col.classList.add(`col-sm-${divisor}`);
    if (i % 2 === 0) {
      col.innerHTML = `col-sm-${divisor}`;
    } else {
      col.innerHTML = `<mark>col-sm-${divisor}</mark>`;
    }
    row.appendChild(col);
  }
  container.appendChild(row);
});