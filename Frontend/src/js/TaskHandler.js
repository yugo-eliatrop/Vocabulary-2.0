import { direction } from "./const";

class TaskHandler {
  constructor(buffSize = 20) {
    this.words = [];
    this.nextIndex = 0;
    this.buff = buffSize;
    this.engToRusMode = null;
    this.direction = direction.free;
    this.loadWords();
  }

  setDirection(dir) {
    this.direction = dir;
  }

  loadWords = () => {
    fetch("LoadWords")
      .then(response => response.json().then(data => {
        if (response.ok) {
          this.words = data;
          this.nextIndex = 0;
          this.setNextTask();
        }
        else
          document.getElementById("task-word").parentElement.innerHTML = data.error;
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
    document.getElementById("task-word").innerHTML = `${task} <span>${word.points}</span>`;
    this.nextIndex++;
  }

  help = () => {
    const word = this.words[this.nextIndex - 1];
    document.getElementById("task-word").innerHTML = `${word[this.engToRusMode ? "rus" : "eng"]} <span>${word.points}</span>`;
    this.engToRusMode = !this.engToRusMode;
  }
}

export default TaskHandler;
