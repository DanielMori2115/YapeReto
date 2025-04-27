CREATE TABLE IF NOT EXISTS dbo."Outbox_Message"
(
    "Event_Id" bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    "Event_Date" date NOT NULL,
    "IsMessageDispatched" bit(1) NOT NULL,
    "Event_Payload" text COLLATE pg_catalog."default" NOT NULL
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS dbo."Outbox_Message"
    OWNER to postgres;

CREATE TABLE IF NOT EXISTS dbo."Transaction"
(
    "Transaction_Id" bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    "Transaction_Date" date NOT NULL,
    "Transaction_Type_Id" integer NOT NULL,
    "Value" numeric NOT NULL,
    "Source_Account_Id" text COLLATE pg_catalog."default" NOT NULL,
    "Target_Account_Id" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Transaction_pkey" PRIMARY KEY ("Transaction_Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS dbo."Transaction"
    OWNER to postgres;

INSERT INTO dbo."Transaction"(
"Transaction_Date", "Transaction_Type_Id", "Value", "Source_Account_Id", "Target_Account_Id")
VALUES (NOW(), 1, 12, 'test1', 'test2');

