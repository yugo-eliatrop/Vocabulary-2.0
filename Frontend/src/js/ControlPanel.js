import { direction } from "./const";

const texts = Object.freeze({
  free: "FREE",
  toEng: "Rus -> Eng",
  toRus: "Eng -> Rus"
});

class ControlPanel {
  constructor(taskHandler) {
    this.panel = document.getElementById("control-panel");
    this.button = this.addDirectionBtn();
    this.direction = direction.free;
    this.taskHandler = taskHandler;
    // let indicator = document.createElement("div");
    // indicator.classList.add("col-6");
  }

  addDirectionBtn() {
    let button = document.createElement("button");
    button.innerHTML = texts.free;
    button.classList.add("btn");
    button.classList.add("btn-primary");
    button.addEventListener("click", () => this.setNextDirection());
    button.style.width = "130px";
    this.panel.appendChild(button);
    return button;
  }

  setNextDirection() {
    if (this.direction === direction.free)
      this.direction = direction.toEng;
    else if (this.direction === direction.toEng)
      this.direction = direction.toRus;
    else if (this.direction === direction.toRus)
      this.direction = direction.free;
    this.taskHandler.setDirection(this.direction);
    this.button.innerHTML = texts[this.direction];
  }
}

export default ControlPanel;
