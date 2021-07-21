import { direction, taskScope } from "./const";

const texts = Object.freeze({
  free: "FREE",
  toEng: "Rus -> Eng",
  toRus: "Eng -> Rus"
});

const scopeTexts = Object.freeze({
  [taskScope.all]: "All words",
  [taskScope.learned]: "Learned words",
  [taskScope.notLearned]: "Unlearned words"
});

class ControlPanel {
  constructor(taskHandler) {
    this.panel = document.getElementById("control-panel");
    this.taskHandler = taskHandler;
    this.directionButton = this.addDirectionBtn();
    this.direction = direction.free;
    this.scope = taskScope.notLearned;
    this.scopeButton = this.addScopeBtn();
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
    button.style.marginRight = "15px";
    this.panel.appendChild(button);
    return button;
  }

  addScopeBtn() {
    let button = document.createElement("button");
    button.innerHTML = scopeTexts[this.scope];
    button.classList.add("btn");
    button.classList.add("btn-primary");
    button.addEventListener("click", () => this.setNextScope());
    button.style.width = "160px";
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
    this.directionButton.innerHTML = texts[this.direction];
  }

  setNextScope() {
    if (this.scope === taskScope.all)
      this.scope = taskScope.learned;
    else if (this.scope === taskScope.learned)
      this.scope = taskScope.notLearned;
    else if (this.scope === taskScope.notLearned)
      this.scope = taskScope.all;
    this.taskHandler.setScope(this.scope);
    this.scopeButton.innerHTML = scopeTexts[this.scope];
  }
}

export default ControlPanel;
