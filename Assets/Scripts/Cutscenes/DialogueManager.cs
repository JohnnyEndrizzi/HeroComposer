using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private Queue<Sentence> sentences = new Queue<Sentence>();

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;

    public RawImage leftCharacter;
    public Animator leftCharacterAnimator;
    public RawImage rightCharacter;
    public Animator rightCharacterAnimator;

    public Animator dialogueBoxAnimator;

    private string prevSpeakerName;

    private List<Coroutine> coroutines = new List<Coroutine>();

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBoxAnimator.SetBool("IsOpen", true);
        sentences.Clear();
        foreach(Sentence sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        StopRunningCoroutines();
        coroutines.Add(StartCoroutine(FadeIn(leftCharacter)));
        coroutines.Add(StartCoroutine(FadeIn(rightCharacter)));
        leftCharacterAnimator.SetBool("IsFocus", true);
        rightCharacterAnimator.SetBool("IsFocus", false);
        prevSpeakerName = sentences.Peek().speaker;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        Sentence sentence = sentences.Dequeue();
        if(!sentence.speaker.Equals(prevSpeakerName))
        {
            if(leftCharacterAnimator.GetBool("IsFocus") == true)
            {
                leftCharacterAnimator.SetBool("IsFocus", false);
                rightCharacterAnimator.SetBool("IsFocus", true);
            }
            else
            {
                leftCharacterAnimator.SetBool("IsFocus", true);
                rightCharacterAnimator.SetBool("IsFocus", false);
            }
            prevSpeakerName = sentence.speaker;
        }
        speakerText.text = sentence.speaker;
        StopCoroutine("TypeSentence");
        StartCoroutine(TypeSentence(sentence.body));
    }

    private void StopRunningCoroutines()
    {
        foreach (Coroutine c in coroutines)
        {
            StopCoroutine(c);
        }
        coroutines.Clear();
    }

    IEnumerator TypeSentence (string sentenceBody)
    {
        dialogueText.text = "";
        foreach(char letter in sentenceBody.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    IEnumerator FadeIn(RawImage image)
    {
        for (float f = 0f; f <= 1; f += 0.1f)
        {
            f = (f+0.1f > 1) ? 1 : f;
            Color c = image.material.color;
            c.a = f;
            image.material.color = c;
            yield return new WaitForSeconds(.025f);
        }
    }

    IEnumerator FadeOut(RawImage image)
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            f = (f-0.1f < 0) ? 0 : f;
            Color c = image.material.color;
            c.a = f;
            image.material.color = c;
            yield return new WaitForSeconds(.025f);
        }
    }

    public void EndDialogue()
    {
        dialogueBoxAnimator.SetBool("IsOpen", false);
        StopRunningCoroutines();
        coroutines.Add(StartCoroutine(FadeOut(leftCharacter)));
        coroutines.Add(StartCoroutine(FadeOut(rightCharacter)));
    }
}
