const { PDFDocument, rgb, degrees } = PDFLib;

let certt = document.querySelector("#cert");
let courseName = certt.getAttribute("data-target-course");
let student = certt.getAttribute("data-target-name");
let date = certt.getAttribute("data-target-date");
let code = certt.getAttribute("data-target-code");



const generatePDFF = async (name, name2, name3,name4) => {
  const { PDFDocument, rgb, degrees } = PDFLib;

    const exBytes = await fetch("https://localhost:44398/assets/cert.pdf").then((res) => {
    return res.arrayBuffer();
  });

  const exFont = await fetch("./Sanchez-Regular.ttf").then((res) =>
    res.arrayBuffer()
  );

  const pdfDoc = await PDFDocument.load(exBytes);
  // pdfDoc.registerFontkit(fontKit);
  // const myFont = await pdfDoc.embedFont(exFont);

  const pages = pdfDoc.getPages();
  const firstPg = pages[0];

  firstPg.drawText(name, {
    x: 260,
    y: 360,
    size: 38,
    color: rgb(0.75, 0.59, 0.36),
  });
  firstPg.drawText(name2, {
    x: 260,
    y: 280,
    size: 38,
    color: rgb(0.75, 0.59, 0.36),
  });
  firstPg.drawText(name3, {
    x: 290,
    y: 205,
    size: 25,
      color: rgb(0.75, 0.59, 0.36),
    
  });
    firstPg.drawText(name4, {
        x: 430,
        y: 530,
        size: 25,
        color: rgb(0.75, 0.59, 0.36),
    });
  const uri = await pdfDoc.saveAsBase64({ dataUri: true });
  document.querySelector("#cert").src = uri;
};
generatePDFF(courseName, student, date, code);

// init();
