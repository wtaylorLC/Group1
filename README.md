# Group1

## Project Brief - Film review website

I want a website that lets users review films. I’m a big fan of films myself. I’d like to add a description of the film myself to get the ball rolling, and then let other users add their reviews about that film. So we would end up with each film having a page of some sort that listed all the reviews. I’d also like pages about actors and directors, which would link to all the films they had appeared in or made. I’d like users to be able to add comments about the actors and directors, too.

I’d like users to be able to reply to each other’s comments, too. But I’d also like to be able to moderate and delete user comments, and block users who make too many out of order comments, as well.

I’d like each review to have a “marks out of ten”, too, so that there’s a league table of films that you can use to see the most / least popular. 

I’d also like people to be able to tag films by their genre (action, thriller, comedy etc), and then use those tags to be able to filter the best / worst tables (so you could list “the worst comedies”).

Plus I’d like people to be able to add film news stories, gossip etc.

##  Contribution Guide

### Branches

**master branch** you will never touch it, this is production and should be updated *rarely* when everything is tested and works.

**development branch** this is development branch, you **do not** code here, you send pull requests here to be checked by other members of your team and merged. If you pair a lot and the dev team is ok with it you can skip the checked part and merge the pull request yourself but still, don’t push you code here.

#### Naming branches

Naming branches is an important aspect of coding with other devs and even for your future self, let’s look at how we can approach this.

*myNewBranch* doesn’t explain anything and it doesn’t solve our problems, we need a naming convention(if needed include your name before the type of the branch like so name-type/short-description):

```
"bug/fixed-all-caps"
"feature/giant-duck-modal"
"refactor/add-prop-types"
"style/everything-is-black"
```

Type/short-description

#### Types

We’ll define 4 *basic types* of branches: **bug**, **feature**, **refactor** and **style**:respectively for bugfixes, new features, code refactoring and design/css stuff, after the type comes the name, it should specify on top of the branch type.