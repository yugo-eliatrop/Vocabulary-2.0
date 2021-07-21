import { direction, taskScope } from "./const";

const checkBoxId = "learned-check-box";
const taskWordId = "task-word";

class TaskHandler {
  constructor(buffSize = 20) {
    this.words = [];
    this.nextIndex = 0;
    this.buff = buffSize;
    this.engToRusMode = null;
    this.direction = direction.free;
    this.scope = taskScope.notLearned;
    this.loadWords();
    document.getElementById(checkBoxId)
      .addEventListener("change", (e) => this.setLearnStatus(e.target.checked));
  }

  setDirection(dir) {
    this.direction = dir;
  }

  setLearnStatus(isLearned) {
    let id = this.words[this.nextIndex - 1].id;
    let body = new FormData();
    body.append("id", id);
    body.append("isLearned", isLearned);
    fetch("/Words/SetLearnStatus", { method: "POST", body })
      .then(response => {
        if (response.ok)
          document.getElementById(checkBoxId).checked = isLearned;
      });
  }

  setScope(scope) {
    this.scope = scope;
    this.loadWords();
  }

  loadWords = () => {
    let body = new FormData();
    body.append("scope", this.scope);
    fetch("LoadWords", { method: "POST", body })
      .then(response => response.json().then(data => {
        if (response.ok) {
          this.words = data;
          this.nextIndex = 0;
          this.setNextTask();
        }
        else
          document.getElementById(taskWordId).parentElement.innerHTML = data.error;
      }));
  }

  sendAnswer = async (success) => {
    let body = new FormData();
    let points = this.words[this.nextIndex - 1].points;
    body.append("id", this.words[this.nextIndex - 1].id);
    body.append("successful", success);
    let response = await fetch("Index", { method: "POST", body });
    if (response.ok) {
      if (this.nextIndex < this.words.length)
        this.setNextTask();
      else
        this.loadWords();
      if (window.statisticHandler)
        window.statisticHandler.update(points, success);
    }
  }

  setNextTask = () => {
    const word = this.words[this.nextIndex];
    this.engToRusMode = this.direction === direction.free ? Math.random() > 0.5 : this.direction === direction.toRus;
    const task = this.engToRusMode ? word.eng : word.rus;
    document.getElementById(taskWordId).innerHTML = `${task} <span>${word.points}</span>`;
    document.getElementById(checkBoxId).checked = word.isLearned;
    this.nextIndex++;
  }

  help = () => {
    const word = this.words[this.nextIndex - 1];
    document.getElementById(taskWordId).innerHTML = `${word[this.engToRusMode ? "rus" : "eng"]} <span>${word.points}</span>`;
    this.engToRusMode = !this.engToRusMode;
  }
}

export default TaskHandler;
