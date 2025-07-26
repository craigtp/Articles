# Effective Bug Reports

When reporting bugs in software, it is essential that the bug reports follow some standardised guideline to ensure the effectiveness of the report.  The first aim of a bug report is to let the programmer see the failure with their own eyes, so you will need to provide detailed instructions so that they can make it fail for themselves.

Every good bug report needs exactly 3 things:

1. **The exact steps required to reproduce the bug**
2. **What you expected to see**
3. **What you actually saw**

The steps to reproduce should be documented clearly and in complete detail, avoiding ambiguous terms and descriptions.  Be careful not to make any assumptions about existing data or software configuration, the exact steps to reproduce the bug should include all necessary details for the specific configuration of software and creation of data required.  Sometimes it's difficult to provide exact steps to perfectly reproduce some bugs, as they can be intermittent in nature.  In this case, exact steps should be supplied anyway, with an additional note that the bug only occurs some of the time.

Expected and observed outcomes should also be documented clearly and in detail.  If you can provide screenshots or animations of the bug actually happening, using free tools such as [Greenshot](http://getgreenshot.org/) and [LICECap](http://www.cockos.com/licecap/), that can greatly help to explain the exact problem, but at the very least the expected and observed outcomes should be clearly described and documented.

When writing out your bug report, including all of the details for the three steps, ensure you adhere to the following guidelines:

 - **Be Specific.** If you can do the same thing two different ways, state which one you used. "I selected Load" might mean "I clicked on Load" or "I pressed Alt-L". Say which you did. Sometimes it matters.
 - **Be Verbose.**  Give more information rather than less. If you say too much, the programmer can ignore some of it. If you say too little, they have to come back and ask more questions.
 - **Be careful of pronouns.** Don't use words like "it", or references like "the window", when it's unclear what they mean. Consider this: "I started FooApp. It put up a warning window. I tried to close it and it crashed." It isn't clear what the user tried to close. Did they try to close the warning window, or the whole of FooApp? It makes a difference. Instead, you could say "I started FooApp, which put up a warning window. I tried to close the warning window, and FooApp crashed." This is longer and more repetitive, but also clearer and less easy to misunderstand.
 -  **Read what you wrote.** Read the report back to yourself, and see if you think it's clear. If you have listed a sequence of actions which should produce the failure, try following them yourself, to see if you missed a step and pay particular attention to ensure that you're not relying on existing state or data that is undocumented within your report and exists only on your computer.