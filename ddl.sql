-- public."Heroes" definição

-- Drop table

-- DROP TABLE public."Heroes";

CREATE TABLE public."Heroes" (
	"Id" int4 GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	"Name" varchar NOT NULL,
	"HeroName" varchar NOT NULL,
	"DateOfBirth" date NOT NULL,
	"Height" numeric NOT NULL,
	"Weight" numeric NOT NULL,
	"Description" varchar NOT NULL,
	CONSTRAINT heroes_pk PRIMARY KEY ("Id"),
	CONSTRAINT heroes_unique UNIQUE ("Name")
);


-- public."Superpowers" definição

-- Drop table

-- DROP TABLE public."Superpowers";

CREATE TABLE public."Superpowers" (
	"Id" int4 GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	"Name" varchar NOT NULL,
	"Description" varchar NULL,
	CONSTRAINT superpowers_pk PRIMARY KEY ("Id")
);


-- public."HeroSuperpowers" definição

-- Drop table

-- DROP TABLE public."HeroSuperpowers";

CREATE TABLE public."HeroSuperpowers" (
	"HeroId" int4 NULL,
	"SuperpowerId" int4 NULL,
	CONSTRAINT herosuperpowers_heroes_fk FOREIGN KEY ("HeroId") REFERENCES public."Heroes"("Id"),
	CONSTRAINT herosuperpowers_superpowers_fk FOREIGN KEY ("SuperpowerId") REFERENCES public."Superpowers"("Id")
);
